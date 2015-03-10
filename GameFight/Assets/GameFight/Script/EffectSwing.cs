using UnityEngine;
using System.Collections;

public class EffectSwing : MonoBehaviour {

	private int uvAnimationTileX;
	private int uvAnimationTileY;
	private int framesPerSecond = 20;
	private int index;
	private int oldindex = -1;
	private float starttime;
	private int lastframe;
	private bool efon;
	private float delay;
	private int impactframe = 1;
	private Rigidbody cha_rigidbody;
	public Transform pt_hit;
	private Vector2 size;
	private Vector2 offset;
	private float uIndex;
	private int vIndex;
	private int originlayer = 20;
	private short layerindex;
	private bool layerchange;
	private bool pton;
	private short rndefamount;
	private int addforce;
	private Renderer myrenderer;
	private Collider mycollider;
	private void Awake()
	{
		this.myrenderer = base.renderer;
		this.mycollider = base.collider;
		this.originlayer = base.gameObject.layer;
	}
	private void Start()
	{
		GameObject gameObject = base.transform.root.gameObject;
		this.cha_rigidbody = gameObject.rigidbody;
		Physics.IgnoreCollision(gameObject.collider, this.mycollider);
		this.starttime = 0f;
		this.myrenderer.enabled = false;
		//this.mycollider.isTrigger = true;
		//this.mycollider.enabled = false;
		this.delay = 0f;
		//base.gameObject.active = false;
	}
	public void SwingOn(float _delay, int cnt_x, int cnt_y, int uvspeed, int impact, int _addforce)
	{
		base.gameObject.layer = this.originlayer;
		base.gameObject.active = true;
		this.delay = _delay;
		this.efon = true;
		this.uvAnimationTileX = cnt_x;
		this.uvAnimationTileY = cnt_y;
		this.lastframe = this.uvAnimationTileX * this.uvAnimationTileY;
		this.framesPerSecond = uvspeed;
		this.impactframe = impact;
		this.addforce = _addforce;
		this.size = new Vector2(1f / (float)this.uvAnimationTileX, 1f / (float)this.uvAnimationTileY);
		if (this.layerchange)
		{
			int num = UnityEngine.Random.Range(0, 100);
			if (num < (int)this.rndefamount)
			{
				base.gameObject.layer = (int)this.layerindex;
			}
		}
	}
	public void SwingOff()
	{
		this.efon = false;
		base.gameObject.active = false;
		this.myrenderer.enabled = false;
		this.mycollider.enabled = false;
		this.pton = false;
		this.oldindex = -1;
		this.index = 0;
		this.starttime = 0f;
	}
	public void RndEfOn(int _index, int _amount)
	{
		this.rndefamount = (short)_amount;
		switch (_amount)
		{
		case 2:
			this.rndefamount = 5;
			break;
		case 3:
			this.rndefamount = 10;
			break;
		case 4:
			this.rndefamount = 20;
			break;
		}
		this.layerchange = true;
		switch (_index)
		{
		case 0:
			this.layerindex = 20;
			this.layerchange = false;
			break;
		case 1:
			this.layerindex = 30;
			break;
		case 2:
			this.layerindex = 31;
			break;
		case 3:
			this.layerindex = 18;
			break;
		case 4:
			this.layerindex = 19;
			break;
		}
	}
	private void Update()
	{
		if (this.efon)
		{
			if (this.delay > 0f)
			{
				this.delay -= Time.deltaTime;
			}
			else
			{
				if (this.addforce > 0)
				{
					this.cha_rigidbody.AddForce(base.transform.root.forward * (float)this.addforce);
				}
				this.myrenderer.enabled = true;
				this.starttime = 0f;
				this.efon = false;
			}
		}
		if (this.myrenderer.enabled)
		{
			this.starttime += Time.deltaTime;
			this.index = (int)(this.starttime * (float)this.framesPerSecond);
			this.uIndex = (float)(this.index % this.uvAnimationTileX);
			this.vIndex = this.index / this.uvAnimationTileX;
			if (this.index != this.oldindex)
			{
				if (this.index >= this.lastframe)
				{
					this.myrenderer.enabled = false;
					base.gameObject.active = false;
					this.mycollider.enabled = false;
					this.pton = false;
					this.oldindex = -1;
				}
				else
				{
					if (this.index == this.impactframe || this.index == this.impactframe + 1)
					{
						this.mycollider.enabled = true;
						if (!this.pton)
						{
							this.pton = true;
						}
					}
					else
					{
						this.mycollider.enabled = false;
					}
				}
				this.offset = Vector2.right * this.uIndex * this.size.x + Vector2.up * (1f - this.size.y - (float)this.vIndex * this.size.y);
				this.myrenderer.material.mainTextureOffset = this.offset;
				this.myrenderer.material.mainTextureScale = this.size;
				this.oldindex = this.index;
			}
		}
	}
}

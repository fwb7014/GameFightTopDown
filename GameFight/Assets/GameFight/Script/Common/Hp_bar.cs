using System;
using UnityEngine;

public class Hp_bar : MonoBehaviour
{
    private float _amount;
    private Vector2 amountU;
    private Vector2 amuontV;
    private Transform mytransform;
    private int oldstatus;
    private Vector2[] originUV = new Vector2[4];
    private Transform parentmon;
    private float posY;
    private Mesh thismesh;

    private void Awake()
    {
        this.mytransform = base.transform;
        this.thismesh = base.GetComponent<MeshFilter>().mesh;
        this.originUV = this.thismesh.uv;
        this.amuontV = (Vector2) (Vector2.up * 0.25f);
    }

    public void Damaged(int _maxhp, int _hp, Transform _parent, float _height, int _status)
    {
        this.parentmon = _parent;
        if (_maxhp != 0)
        {
            this._amount = (1f - (((float) _hp) / ((float) _maxhp))) * 0.5f;
			_status = 0;
            this.amountU = (Vector2) (Vector2.right * this._amount);
            if (1==2)
            {
                _status = this.oldstatus;
            }
			Debug.Log (this._amount+"____"+_status);
            this.thismesh.uv = new Vector2[] { (this.originUV[0] + this.amountU) + (this.amuontV * _status), (this.originUV[1] + this.amountU) + (this.amuontV * _status), (this.originUV[2] + this.amountU) + (this.amuontV * _status), (this.originUV[3] + this.amountU) + (this.amuontV * _status) };
        }
        this.posY = _height;
        this.oldstatus = _status;
    }

    public void FreeSelect()
    {
        this.mytransform.position = (Vector3) (Vector3.one * 4f);
        this.parentmon = null;
    }

    private void Update()
    {
        if (this.parentmon != null)
        {
            this.mytransform.position = this.parentmon.position + new Vector3(0f, this.posY, -0.02f);
        }
    }
}


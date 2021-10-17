using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Water.Scripts.Manager;

namespace Object
{
    public enum BuildState
    {
        None,
        Blueprint,
        Complete,
    }
    public abstract class TeamObject : MonoBehaviour
    {
        [SerializeField] private BuildState buildState = BuildState.None;
        
        [SerializeField] private int hp;
        [SerializeField] private int maxhp;
        [SerializeField] private int armor;
        [SerializeField] private int team;

        [SerializeField] private Camera uiCamera;
        [SerializeField] private RectTransform rectParent;
        
        [SerializeField]private Slider hpBar; 
        [SerializeField] private Vector3 hpBar_Offset;
        [SerializeField]private RectTransform hpBar_Rect;

        [SerializeField] private Text nameText;
        
        #region Property Funcs
        public BuildState GetBuildState()
        {
            return buildState;
        }

        public void SetBuildState(BuildState state)
        {
            buildState = state;

            switch (state)
            {
                case BuildState.None:
                    break;
                case BuildState.Complete:
                    OnBuildComplete();
                    break;
                case BuildState.Blueprint:
                    break;
            }
        }
        
        public int GetHealth()
        {
            return hp;
        }

        public void SetHealth(int num)
        {
            hp = Mathf.Max(0, num);
            hpBar.value = (float)hp / (float)maxhp * 100F;
        }

        public void ResetHealth()
        {
            SetHealth(Mathf.Max(0,maxhp));
        }

        public int GetMaxHealth()
        {
            return maxhp;
        }

        public void SetMaxHealth(int num)
        {
            maxhp = Mathf.Max(0,num);
            hpBar.value = (hp / maxhp) * 100;
        }

        public int GetArmor()
        {
            return armor;
        }

        public void SetArmor(int num)
        {
            armor = num;
        }

        public int GetTeam()
        {
            return team;
        }

        public void SetTeam(int num)
        {
            team = num;
        }

        public bool IsTeam(int num)
        {
            return team == num;
        }

        public bool IsTeam(TeamObject obj)
        {
            return obj.GetTeam() == team;
        }

        public bool IsEnemy(TeamObject obj)
        {
            return obj.GetTeam() != team;
        }
        #endregion

        #region Callback
        public virtual void OnInit()
        {
            hpBar.fillRect.GetComponent<Image>().color = DataManager.Instance.Get_TeamColor()[GetTeam()];
        }
        
        public virtual void OnRemove()
        {
            
        }
        
        public virtual void OnBuildComplete()
        {
            
        }

        public virtual void OnTakeDamage()
        {
            
        }

        public virtual void OnThink()
        {
            if (hpBar != null && hpBar_Rect != null)
            {
                var scrPos = Camera.main.WorldToViewportPoint(transform.position + hpBar_Offset);

                if (scrPos.z < 0F)
                {
                    scrPos *= -1F;
                }

                var pos = new Vector2((scrPos.x * rectParent.sizeDelta.x) - (rectParent.sizeDelta.x * 0.5F),
                    (scrPos.y * rectParent.sizeDelta.y) - (rectParent.sizeDelta.y * 0.5F));

                hpBar_Rect.anchoredPosition = pos;
            }
        }
        #endregion

        #region Custom Funcs
        public void Remove(bool isDestroy)
        {
            if (isDestroy)
            {
                Destroy(this.gameObject);  
            }
            else
            {
                this.gameObject.SetActive(false);   
            }
            OnRemove();
        }

        public void TakeDamage(DamageInfo dmgInfo)
        {
            SetHealth(GetHealth() - dmgInfo.damage);

            if (hp <= 0)
            {
                Remove(false);
            }
            
            OnTakeDamage();
        }
        
        public void TakeDamage(int dmg)
        {
            var dmgInfo = new DamageInfo();
            dmgInfo.damage = dmg;
            
            SetHealth(GetHealth() - dmgInfo.damage);

            if (hp <= 0)
            {
                Remove(false);
            }
            
            OnTakeDamage();
        }
        #endregion
        
        #region Unity Default Funcs 
        private void Awake()
        {
            OnInit();
        }

        private void Update()
        {
            OnThink();
        }

        private void LateUpdate()
        {

        }

        #endregion
    }
}

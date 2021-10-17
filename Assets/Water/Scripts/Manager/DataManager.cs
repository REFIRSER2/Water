using UnityEngine;

namespace Water.Scripts.Manager
{
    public class DataManager : MonoBehaviour
    {
        #region Singletone
        private static DataManager instance;
        public static DataManager Instance;
        #endregion
    
        #region Unity General Funcs
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

            Setup_TeamColor();
        }
        #endregion
        
        #region Color
        private Color[] team_Color = new Color[2];

        private void Setup_TeamColor()
        {
            team_Color[Define_Table.TEAM_RED] = Color.red;
            team_Color[Define_Table.TEAM_BLUE] = Color.blue;
        }
        
        public Color[] Get_TeamColor()
        {
            return team_Color;
        }
        #endregion
    }
}

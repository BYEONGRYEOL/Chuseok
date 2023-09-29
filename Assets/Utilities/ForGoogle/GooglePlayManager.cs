/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Threading.Tasks;
using UnityEngine.UI;

using 
Isometric.Levels;
namespace Isometric.Utility
{

    public class GooglePlayManager : SingletonDontDestroyMonobehavior<GooglePlayManager>
    {
        [SerializeField] Text txtLog;
        bool isNew;
        int nScore;
        int nIQ;
        int nAchievement;
        [SerializeField] Text txtScore;
        [SerializeField] Text txtIQ;
        [SerializeField] Text txtAchievement;

        
        private readonly string LOG = $"[Google]";

        public void InitGoogleService()
        {
            PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().RequestIdToken().RequestEmail().Build());
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();

        }

        public async Task<string> GoogleServiceLogin()
        {
            TaskCompletionSource<string> task = new TaskCompletionSource<string>();
            if (!Social.localUser.authenticated)
            {
                Social.localUser.Authenticate(success =>
                {
                    if (success)
                    {
                        Debug.Log($"{LOG} GooglePlay Login Success");
                        string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
                        task.SetResult(idToken);
                    }
                    else
                    {
                        Debug.Log($"{LOG} Fail");
                    }
                });
            }
            return await task.Task;
        }

        public void GoogleLogOut()
        {
            if (false == Social.localUser.authenticated)
                return;
            PlayGamesPlatform.Instance.SignOut();
        }

        public void ShowAchievement()
        {
            if (false == Social.localUser.authenticated)
                return;
            Social.ShowAchievementsUI();
        }

        public void ShowLeaderBoard()
        {
            if (false == Social.localUser.authenticated)
                return;
            //ReprotScore(������ ���� ��, �������� id��
            Social.ReportScore(MissionObjectList.Instance.TotalStars(), GPGSIds.leaderboard, (bool isSuccess) => { });
            Social.ShowLeaderboardUI();
        }

        protected override void Awake()
        {
            base.Awake();
            PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();

            isNew = true;
            nScore = 0;
            nIQ = 0;
            nAchievement = 0;
        }
        
      
        

        public void CompleteAchivement_1()
        {
            //�������� ��� Ŭ����
            Social.ReportProgress(GPGSIds.achievement, 100f, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Achievement_1 �޼�");
                }
            });
        }
        public void CompleteAchivement_2()
        {
            //�븻���� ��� Ŭ����
            Social.ReportProgress(GPGSIds.achievement_2, 100f, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Achievement_2 �޼�");
                }
            });
        }
        public void CompleteAchivement_3()
        {
            //�ϵ巹�� ��� Ŭ����
            Social.ReportProgress(GPGSIds.achievement_3, 100f, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Achievement_3 �޼�");
                }
            });
        }
        public void CompleteAchivement_4()
        {
            //UIAbout �湮 ��
            Social.ReportProgress(GPGSIds.achievement_4, 100f, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Achievement_4 �޼�");
                }
            });
        }
        public void CompleteAchivement_5()
        {
            //�� 30��
            Social.ReportProgress(GPGSIds.achievement_5, 100f, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Achievement_5 �޼�");
                }
            });
        }
    }
}

*/
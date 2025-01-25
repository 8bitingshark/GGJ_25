using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Managers
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        private int _currentScore = 0;
        private int _targetScore = 0;

        // to handle score ui
        public EventHandler<OnScoreChangedEventArgs> OnScoreChanged;

        public class OnScoreChangedEventArgs : EventArgs
        {
            public float scoreNormalized;
        }
        
        


    }
}

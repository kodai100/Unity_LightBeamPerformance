namespace ProjectBlue.LightBeamPerformance
{
    
    public abstract class AnimationBase
    {
        private BPM bpm;
        protected float time;
        protected float beat;
        
        protected AnimationBase(BPM bpm)
        {
            this.bpm = bpm;
        }
        
        public void Update(float elapsedTime)
        {
            time = elapsedTime;
            beat = bpm.SecondsToBeat(elapsedTime);
        }
        

    }

}
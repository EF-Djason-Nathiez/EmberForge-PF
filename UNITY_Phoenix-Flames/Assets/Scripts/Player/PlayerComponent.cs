namespace Player
{
    public abstract class PlayerComponent
    {
        protected PlayerManager manager;

        public virtual void Initialize(PlayerManager pm)
        {
            manager = pm;
        }
    }
}

namespace UnknownGame
{
    public class GameObject
    {
        #region Constructors

        public GameObject()
        {
            Transform = new Transform();
        }

        #endregion

        #region Properties

        public Transform Transform { get; set; }

        #endregion
    }
}

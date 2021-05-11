public interface IState
{
    //Needs a constructor, obviously
    //And these Methods:
    void Tick();
    void OnEnter();
    void OnExit();
}

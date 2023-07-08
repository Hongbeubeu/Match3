using Ultimate.Core.Runtime.Singleton;

public class GameManager : Singleton<GameManager>
{
    public GameConfig GameConfig;

    public ObjectPooler ObjectPooler;
    
    
    public override void Init()
    {
    }
}

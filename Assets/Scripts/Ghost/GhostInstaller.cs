using Zenject;
public class GhostInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Ghost>().FromComponentInHierarchy().AsSingle();
        
        Container.Bind<MovementController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GhostHome>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GhostFrightened>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GhostChase>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GhostScatter>().FromComponentInHierarchy().AsSingle();
    }
}

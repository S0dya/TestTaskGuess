using Game;
using Game.Grid;
using Game.UI;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GridController>().FromComponentInHierarchy().AsSingle();

        Container.Bind<UITaskDescriptionController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UIParticleEffects>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UILevelFinish>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UILoadingScreen>().FromComponentInHierarchy().AsSingle();
    }
}
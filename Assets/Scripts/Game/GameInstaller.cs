using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GridController>().FromComponentInHierarchy().AsSingle();

        Container.Bind<UIInGame>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UITaskDescriptionController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UIParticleEffects>().FromComponentInHierarchy().AsSingle();
    }
}
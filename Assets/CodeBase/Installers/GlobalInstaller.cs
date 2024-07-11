using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private CharacterControl _player;

    public override void InstallBindings()
    {
        Container.Bind<CharacterInventory>().AsSingle();

        Container.BindInstance(new PlayerInput()).AsSingle();
        Container.BindInstance(_player).AsSingle();
    }
}
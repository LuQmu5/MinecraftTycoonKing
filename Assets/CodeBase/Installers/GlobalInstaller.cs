﻿using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInstance(new PlayerInput()).AsSingle();
    }
}
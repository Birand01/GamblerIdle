using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DependencyInjector : MonoInstaller
{
    
    public override void InstallBindings()
    {
        Container.Bind<PlayerMovement>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<DynamicJoystick>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<HammerDurability>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<MoneyManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        //Container.Bind<FortuneWheelManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<BetAmount>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<Wheel>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}

using RedWoods.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RedWoodInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IUserInputService>().To<UserInputService>().AsSingle();
        Container.Bind<ITimeService>().To<TimeService>().AsSingle();
    }
}
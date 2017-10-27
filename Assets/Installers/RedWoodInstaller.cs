using RedWoods.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Class for establishing Dependency Injection Bindings
/// Used by the Zenject SceneContext object in the Unity Scene as an installer
/// </summary>
public class RedWoodInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IUserInputService>().To<UserInputService>().AsSingle();
        Container.Bind<ITimeService>().To<TimeService>().AsSingle();
    }
}
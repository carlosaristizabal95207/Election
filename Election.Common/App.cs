﻿namespace Election.Common
{
    using ViewModel;
    using MvvmCross.IoC;
    using MvvmCross.ViewModels;

    public class App : MvxApplication
    {
        public override void Initialize()
        {
            this.CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            this.RegisterAppStart<LoginViewModel>();
        }
    }
}
﻿using Autofac;

namespace EYM.Tests
{
	class TestsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			/*
			Register your project dependencies here. Autofac will automatically process them on MVC app start
			Syntax example:
			builder.Register(c => new MyClass()).As<IMyInterface>();
			*/
		}
	}
}

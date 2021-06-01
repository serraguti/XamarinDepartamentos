﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinDepartamentos.Services
{
    public class ServiceIoC
    {
        private IContainer container;

        public ServiceIoC()
        {
            this.RegisterDependencies();
        }
        private void RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ServiceDepartamentos>();
            builder.RegisterType<DepartamentosViewModel>();
            this.container = builder.Build();
        }

        public DepartamentosViewModel DepartamentosViewModel
        {
            get
            {
                return this.container.Resolve<DepartamentosViewModel>();
            }
        }
    }
}

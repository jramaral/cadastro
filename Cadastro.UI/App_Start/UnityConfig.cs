using System.Web.Mvc;
using Cadastro.Aplicacao;
using Cadastro.Dominio.Interfaces;
using Cadastro.Repositorio.Repositorios;
using Unity;
using Unity.Mvc5;

namespace Cadastro.UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IDiretorRepositorio, DiretorRepositorio>();
            container.RegisterType<DiretorAplicacao>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
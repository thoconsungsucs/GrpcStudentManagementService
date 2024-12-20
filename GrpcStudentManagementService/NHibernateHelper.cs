﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using GrpcStudentManagementService.Models;
using NHibernate;
namespace GrpcStudentManagementService
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory CreateSessionFactory()
        {
            if (_sessionFactory == null)
            {
                try
                {
                    _sessionFactory = Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2012
                            .ConnectionString(c => c
                                .Server("DESKTOP-9P5CFP6")
                                //.Server("NGUYENDUYTHANH")
                                .Database("StudentManagement")
                                .TrustedConnection())
                        .ShowSql())
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Student>())
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Class>())
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Teacher>())
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Grade>())
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Level>())
                        .BuildSessionFactory();
                }
                catch (FluentConfigurationException ex)
                {
                    // Log exception details for debugging
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }
            }
            return _sessionFactory;
        }

        public static NHibernate.ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using ProjetOrion.Models;

namespace ProjetOrion.Tests
{
    [TestFixture]
    public class TestDal
    {
        [SetUp]
        public void Initialize()
        {
            IDatabaseInitializer<AfCompContext> init = new DropCreateDatabaseAlways<AfCompContext>();
            Database.SetInitializer(init);
            init.InitializeDatabase(new AfCompContext());
        }

        [Test]
        public void Test1()
        {
            using (IDal dal = new Dal())
            {
                //dal.CreerRestaurant("La bonne fourchette", "01 02 03 04 05");
                //List<Resto> restos = dal.ObtientTousLesRestaurants();

                //Assert.IsNotNull(restos);
                //Assert.AreEqual(1, restos.Count);
                //Assert.AreEqual("La bonne fourchette", restos[0].Nom);
                //Assert.AreEqual("01 02 03 04 05", restos[0].Telephone);
            }
        }
    }
}

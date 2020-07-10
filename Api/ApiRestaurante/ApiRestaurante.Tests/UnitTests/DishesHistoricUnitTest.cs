using ApiRestaurante.Controllers;
using ApiRestaurante.EnumType;
using ApiRestaurante.Models;
using ApiRestaurante.Repository.Interface;
using ApiRestaurante.Rules.Interface;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using static ApiRestaurante.Controllers.DishesTypeController;

namespace ApiRestaurante.Tests.UnitTests
{
    [TestClass]
    public class DishesHistoricUnitTest
    {
        #region GET Method
        [TestMethod( "Get historic" )]
        public void GetHistoric( )
        {
            EnumTimeDay timeDay = EnumTimeDay.Morning;

            List<DishHistoricEntity> all = new List<DishHistoricEntity>( );
            all.Add(
                new DishHistoricEntity
                {
                    Date = DateTime.UtcNow,
                    Dishes = new List<EnumDishType> { EnumDishType.Dessert }
                }
            );

            Mock<IDishesRepository> mockRepository = new Mock<IDishesRepository>( );
            Mock<ILogger<DishesTypeController>> mockLogger = new Mock<ILogger<DishesTypeController>>( );

            mockRepository
                .Setup( s => s.ExistsTimeDay( timeDay ) )
                .Returns( true );

            mockRepository
                .Setup( s => s.History( timeDay ) )
                .Returns( all );

            var controller = new DishesTypeController(
                mockRepository.Object,
                It.IsAny<IRuleDishesType>( ),
                mockLogger.Object
            );

            var ret = controller.GetHistoric( ( int )timeDay );

            ret.Should( )
                .BeOfType<OkObjectResult>( );

            mockRepository.Verify( s => s.ExistsTimeDay( timeDay ), Times.Once );
            mockRepository.Verify( v => v.History( timeDay ), Times.Once );
        }

        [TestMethod( "Get historic but not exist timeday" )]
        public void GetHistoricButNotExistTimeDay( )
        {
            EnumTimeDay timeDay = EnumTimeDay.Morning;

            Mock<IDishesRepository> mockRepository = new Mock<IDishesRepository>( );
            Mock<ILogger<DishesTypeController>> mockLogger = new Mock<ILogger<DishesTypeController>>( );

            mockRepository
                .Setup( s => s.ExistsTimeDay( timeDay ) )
                .Returns( false );

            var controller = new DishesTypeController(
                mockRepository.Object,
                It.IsAny<IRuleDishesType>( ),
                mockLogger.Object
            );

            var ret = controller.GetHistoric( ( int )timeDay );

            ret.Should( )
                .BeOfType<BadRequestObjectResult>( )
                .Which.Value
                .Should( ).Be( DishesHistoricErrors.TimeDayIdNotFound );

            mockRepository.Verify( s => s.ExistsTimeDay( timeDay ), Times.Once );
            mockRepository.Verify( v => v.History( timeDay ), Times.Never );
        }
        #endregion
    }
}

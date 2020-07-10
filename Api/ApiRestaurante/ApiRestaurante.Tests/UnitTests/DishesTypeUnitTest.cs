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
using System.Collections.Generic;
using System.Linq;
using static ApiRestaurante.Controllers.DishesTypeController;

namespace ApiRestaurante.UnitTests
{
    [TestClass]
    public class DishesTypeUnitTest
    {
        #region GET Method
        [TestMethod( "Get all by timeDayId" )]
        public void GetAllByTimeDayId( )
        {
            EnumTimeDay timeDay = EnumTimeDay.Morning;

            List<DishModel> all = new List<DishModel>( );
            all.Add( new DishModel { TimeDayId = EnumTimeDay.Morning, DishTypeId = EnumDishType.Entree } );
            all.Add( new DishModel { TimeDayId = EnumTimeDay.Morning, DishTypeId = EnumDishType.Side } );
            all.Add( new DishModel { TimeDayId = EnumTimeDay.Morning, DishTypeId = EnumDishType.Drink } );
            all.Add( new DishModel { TimeDayId = EnumTimeDay.Morning, DishTypeId = EnumDishType.Dessert } );

            Mock<IDishesRepository> mockRepository = new Mock<IDishesRepository>( );
            Mock<ILogger<DishesTypeController>> mockLogger = new Mock<ILogger<DishesTypeController>>( );

            mockRepository
                .Setup( s => s.ExistsTimeDay( timeDay ) )
                .Returns( true );

            mockRepository
                .Setup( s => s.GetByTimeDay( timeDay ) )
                .Returns( all );

            var controller = new DishesTypeController(
                mockRepository.Object,
                It.IsAny<IRuleDishesType>(),
                mockLogger.Object
            );

            var ret = controller.GetAllByTimeDayId( ( int )timeDay );

            ret.Should( )
                .BeOfType<OkObjectResult>( )
                .Which.Value
                .Should( ).Be( all );

            mockRepository.Verify( s => s.ExistsTimeDay( timeDay ), Times.Once );
            mockRepository.Verify( v => v.GetByTimeDay( timeDay ), Times.Once );
        }

        [TestMethod( "Get all by timeDayId but not exists time day" )]
        public void GetAllByTimeDayIdButNotExistsTimeDay( )
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

            var ret = controller.GetAllByTimeDayId( ( int )timeDay);

            ret.Should( )
                .BeOfType<BadRequestObjectResult>( )
                .Which.Value
                .Should( ).Be( DishesErrors.TimeDayIdNotFound );

            mockRepository.Verify( s => s.ExistsTimeDay( timeDay ), Times.Once );
            mockRepository.Verify( v => v.GetByTimeDay( timeDay ), Times.Never );
        }

        [TestMethod( "Get dish type by id" )]
        public void GetDishTypeById( )
        {
            EnumTimeDay timeDay = EnumTimeDay.Morning;

            DishModel dishModel = new DishModel { TimeDayId = timeDay, DishTypeId = EnumDishType.Entree };

            Mock<IDishesRepository> mockRepository = new Mock<IDishesRepository>( );
            Mock<ILogger<DishesTypeController>> mockLogger = new Mock<ILogger<DishesTypeController>>( );

            mockRepository
                .Setup( s => s.ExistsTimeDay( timeDay ) )
                .Returns( true );

            mockRepository
                .Setup( s => s.GetByDishTypeId( timeDay, EnumDishType.Entree ) )
                .Returns( dishModel );

            var controller = new DishesTypeController(
                mockRepository.Object,
                It.IsAny<IRuleDishesType>( ),
                mockLogger.Object
            );

            var ret = controller.GetByDishType( ( int )timeDay, ( int )EnumDishType.Entree );

            ret.Should( )
                .BeOfType<OkObjectResult>( )
                .Which.Value
                .Should( ).Be( dishModel );

            mockRepository.Verify( s => s.ExistsTimeDay( timeDay ), Times.Once );
            mockRepository.Verify( v => v.GetByDishTypeId( timeDay, EnumDishType.Entree ), Times.Once );
        }

        [TestMethod( "Get dish type by id but not exists time day" )]
        public void GetDishTypeByIdButNotExistsTimeDay( )
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

            var ret = controller.GetByDishType( ( int )timeDay, ( int )EnumDishType.Entree );

            ret.Should( )
                .BeOfType<BadRequestObjectResult>( )
                .Which.Value
                .Should( ).Be( DishesErrors.TimeDayIdNotFound );

            mockRepository.Verify( s => s.ExistsTimeDay( timeDay ), Times.Once );
            mockRepository.Verify( v => v.GetByDishTypeId( timeDay, EnumDishType.Entree ), Times.Never );
        }

        [TestMethod( "Get dish type by id but no content" )]
        public void GetDishTypeByIdButNoContent( )
        {
            EnumTimeDay timeDay = EnumTimeDay.Morning;

            Mock<IDishesRepository> mockRepository = new Mock<IDishesRepository>( );
            Mock<ILogger<DishesTypeController>> mockLogger = new Mock<ILogger<DishesTypeController>>( );

            mockRepository
                .Setup( s => s.ExistsTimeDay( timeDay ) )
                .Returns( true );

            mockRepository
                .Setup( s => s.GetByDishTypeId( timeDay, It.IsAny<EnumDishType>( ) ) )
                .Returns( It.IsAny<DishModel>( ) );

            var controller = new DishesTypeController(
                mockRepository.Object,
                It.IsAny<IRuleDishesType>( ),
                mockLogger.Object
            );

            var ret = controller.GetByDishType( ( int )timeDay, ( int )EnumDishType.Entree );

            ret.Should( ).BeOfType<NoContentResult>( );

            mockRepository.Verify( s => s.ExistsTimeDay( timeDay ), Times.Once );
            mockRepository.Verify( v => v.GetByDishTypeId( timeDay, EnumDishType.Entree ), Times.Once );
        }
        #endregion

        #region POST Method
        [TestMethod( "Post new order" )]
        public void Post( )
        {
            DishModelRequest dishModel = new DishModelRequest
            {
                Dishes = new List<int>( ) { ( int )EnumDishType.Entree, ( int )EnumDishType.Drink }
            };
            
            EnumTimeDay timeDay = EnumTimeDay.Morning;
            string retNewOrder = "";

            Mock<IDishesRepository> mockRepository = new Mock<IDishesRepository>( );
            Mock<IRuleDishesType> mockRule = new Mock<IRuleDishesType>( );
            Mock<ILogger<DishesTypeController>> mockLogger = new Mock<ILogger<DishesTypeController>>( );

            mockRule
                .Setup( s => s.IsValidRule( It.IsAny<EnumTimeDay>( ), It.IsAny<int>( ) ) )
                .Returns( true );

            mockRepository
                .Setup( s => s.ExistsTimeDay( It.IsAny<EnumTimeDay>( ) ) )
                .Returns( true );

            mockRepository
                .Setup( s => s.GetByDishTypeId( It.IsAny<EnumTimeDay>( ), EnumDishType.Entree ) )
                .Returns( new DishModel { TimeDayId = timeDay, DishTypeId = EnumDishType.Entree } );

            mockRepository
                .Setup( s => s.GetByDishTypeId( It.IsAny<EnumTimeDay>( ), EnumDishType.Drink ) )
                .Returns( new DishModel { TimeDayId = timeDay, DishTypeId = EnumDishType.Drink } );

            mockRepository
                .Setup( s => s.NewOrder( timeDay, It.IsAny<List<DishModel>>( ) ) )
                .Callback<EnumTimeDay, List<DishModel>>( ( timeDayId, list ) =>
                {
                    retNewOrder = string.Join( ", ",
                        list.GroupBy( g => g.DishTypeId ).Select( s =>
                        {
                            string description = "Inválido";
                            DishModel model = s.First( );

                            if ( null != model )
                            {
                                int count = list.Count( c => c.DishTypeId == s.Key );
                                description = $"{model.Dish}{( count > 1 ? $"(X{count})" : "" )}";
                            }

                            return description;
                        } )
                    );
                } )
                .Returns( "Ovos, Café" );

            var controller = new DishesTypeController(
                mockRepository.Object,
                mockRule.Object,
                mockLogger.Object
            );

            var ret = controller.PostOrder( ( int )timeDay, dishModel );

            ret.Should( )
                .BeOfType<OkObjectResult>( )
                .Which.Value.As<DishModelResponse>( )
                .Result
                .Should( ).Be( retNewOrder );

            mockRule.Verify( s => s.IsValidRule( It.IsAny<EnumTimeDay>( ), It.IsAny<int>( ) ), Times.Exactly( 2 ) );
            mockRepository.Verify( s => s.ExistsTimeDay( It.IsAny<EnumTimeDay>( ) ), Times.Once );
            mockRepository.Verify( s => s.GetByDishTypeId( It.IsAny<EnumTimeDay>( ), EnumDishType.Entree ), Times.Once );
            mockRepository.Verify( s => s.GetByDishTypeId( It.IsAny<EnumTimeDay>( ), EnumDishType.Drink ), Times.Once );
            mockRepository.Verify( s => s.NewOrder( timeDay, It.IsAny<List<DishModel>>( ) ), Times.Once );
        }

        [TestMethod( "Post new order but not exists time day" )]
        public void PostButNotExitsTimeDay( )
        {
            EnumTimeDay timeDay = EnumTimeDay.Morning;

            Mock<IDishesRepository> mockRepository = new Mock<IDishesRepository>( );
            Mock<IRuleDishesType> mockRule = new Mock<IRuleDishesType>( );
            Mock<ILogger<DishesTypeController>> mockLogger = new Mock<ILogger<DishesTypeController>>( );

            mockRepository
                .Setup( s => s.ExistsTimeDay( It.IsAny<EnumTimeDay>( ) ) )
                .Returns( false );

            var controller = new DishesTypeController(
                mockRepository.Object,
                mockRule.Object,
                mockLogger.Object
            );

            var ret = controller.PostOrder( ( int )timeDay, It.IsAny<DishModelRequest>( ) );

            ret.Should( )
                .BeOfType<BadRequestObjectResult>( )
                .Which.Value
                .Should( ).Be( DishesErrors.TimeDayIdNotFound );

            mockRule.Verify( s => s.IsValidRule( It.IsAny<EnumTimeDay>( ), It.IsAny<int>( ) ), Times.Never );
            mockRepository.Verify( s => s.ExistsTimeDay( It.IsAny<EnumTimeDay>( ) ), Times.Once );
            mockRepository.Verify( s => s.GetByDishTypeId( It.IsAny<EnumTimeDay>( ), EnumDishType.Entree ), Times.Never );
            mockRepository.Verify( s => s.GetByDishTypeId( It.IsAny<EnumTimeDay>( ), EnumDishType.Drink ), Times.Never );
            mockRepository.Verify( s => s.NewOrder( It.IsAny<EnumTimeDay>( ), It.IsAny<List<DishModel>>( ) ), Times.Never );
        }
        #endregion
    }
}

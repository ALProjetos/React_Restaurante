using ApiRestaurante.Code;
using ApiRestaurante.EnumType;
using ApiRestaurante.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ApiRestaurante.Tests.IntegrationTests
{
    [TestClass]
    public class DishesTypeIntegrationTest : BaseIntegrationTest
    {
        #region GET Method
        [TestMethod( "Get all by time day" )]
        public async Task GetAllBytTimeDay( )
        {
            var response = await m_Client.GetAsync(
                $"api/DishesType/{( int )EnumTimeDay.Morning}"
            );

            Xunit.Assert.True( response.IsSuccessStatusCode );
            List<DishModel> dishes = await GetAs<List<DishModel>>( response );

            Xunit.Assert.True( dishes.Count == 4 );
        }

        [TestMethod( "Get by dish type" )]
        public async Task GetByDishType( )
        {
            var response = await m_Client.GetAsync(
                $"api/DishesType/{( int )EnumTimeDay.Morning}/{( int )EnumDishType.Entree}"
            );

            Xunit.Assert.True( response.IsSuccessStatusCode );
            DishModel dish = await GetAs<DishModel>( response );

            Xunit.Assert.NotNull( dish );
        }
        #endregion

        #region POST Method
        [TestMethod( "Post order morning 1, 2, 3" )]
        public async Task PostOrderMorningTest1( )
        {
            DishModelRequest dishModel = new DishModelRequest
            {
                Dishes = new List<int>( ) { 1, 2, 3 }
            };

            var response = await m_Client.PostAsync(
                $"api/DishesType/{( int )EnumTimeDay.Morning}",
                GetAsJsonContent( dishModel )
            );

            Xunit.Assert.True( response.IsSuccessStatusCode );
            DishModelResponse dishResponse = await GetAs<DishModelResponse>( response );

            Xunit.Assert.NotNull( dishResponse );
            Xunit.Assert.Equal(
                $"{EnumDishesMorning.Eggs.GetEnumDescription( )}," +
                $" {EnumDishesMorning.Toast.GetEnumDescription( )}," +
                $" {EnumDishesMorning.Coffe.GetEnumDescription( )}",
                dishResponse.Result
            );
        }

        [TestMethod( "Post order morning 2, 1, 3" )]
        public async Task PostOrderMorningTest2( )
        {
            DishModelRequest dishModel = new DishModelRequest
            {
                Dishes = new List<int>( ) { 2, 1, 3 }
            };

            var response = await m_Client.PostAsync(
                $"api/DishesType/{( int )EnumTimeDay.Morning}",
                GetAsJsonContent( dishModel )
            );

            Xunit.Assert.True( response.IsSuccessStatusCode );
            DishModelResponse dishResponse = await GetAs<DishModelResponse>( response );

            Xunit.Assert.NotNull( dishResponse );
            Xunit.Assert.Equal(
                $"{EnumDishesMorning.Eggs.GetEnumDescription( )}," +
                $" {EnumDishesMorning.Toast.GetEnumDescription( )}," +
                $" {EnumDishesMorning.Coffe.GetEnumDescription( )}",
                dishResponse.Result
            );
        }

        [TestMethod( "Post order morning 1, 2, 3, 4" )]
        public async Task PostOrderMorningTest3( )
        {
            DishModelRequest dishModel = new DishModelRequest
            {
                Dishes = new List<int>( ) { 1, 2, 3, 4 }
            };

            var response = await m_Client.PostAsync(
                $"api/DishesType/{( int )EnumTimeDay.Morning}",
                GetAsJsonContent( dishModel )
            );

            Xunit.Assert.True( response.IsSuccessStatusCode );
            DishModelResponse dishResponse = await GetAs<DishModelResponse>( response );

            Xunit.Assert.NotNull( dishResponse );
            Xunit.Assert.Equal(
                $"{EnumDishesMorning.Eggs.GetEnumDescription( )}," +
                $" {EnumDishesMorning.Toast.GetEnumDescription( )}," +
                $" {EnumDishesMorning.Coffe.GetEnumDescription( )}," +
                " Não tem sobremesa no café da manhã",
                dishResponse.Result
            );
        }

        [TestMethod( "Post order morning 1, 2, 3, 3, 3" )]
        public async Task PostOrderMorningTest4( )
        {
            DishModelRequest dishModel = new DishModelRequest
            {
                Dishes = new List<int>( ) { 1, 2, 3, 3, 3 }
            };

            var response = await m_Client.PostAsync(
                $"api/DishesType/{( int )EnumTimeDay.Morning}",
                GetAsJsonContent( dishModel )
            );

            Xunit.Assert.True( response.IsSuccessStatusCode );
            DishModelResponse dishResponse = await GetAs<DishModelResponse>( response );

            Xunit.Assert.NotNull( dishResponse );
            Xunit.Assert.Equal(
                $"{EnumDishesMorning.Eggs.GetEnumDescription( )}," +
                $" {EnumDishesMorning.Toast.GetEnumDescription( )}," +
                $" {EnumDishesMorning.Coffe.GetEnumDescription( )}(X3)",
                dishResponse.Result
            );
        }

        [TestMethod( "Post order night 1, 2, 3, 4" )]
        public async Task PostOrderNightTest1( )
        {
            DishModelRequest dishModel = new DishModelRequest
            {
                Dishes = new List<int>( ) { 1, 2, 3, 4 }
            };

            var response = await m_Client.PostAsync(
                $"api/DishesType/{( int )EnumTimeDay.Night}",
                GetAsJsonContent( dishModel )
            );

            Xunit.Assert.True( response.IsSuccessStatusCode );
            DishModelResponse dishResponse = await GetAs<DishModelResponse>( response );

            Xunit.Assert.NotNull( dishResponse );
            Xunit.Assert.Equal(
                $"{EnumDishesNigth.Steak.GetEnumDescription( )}," +
                $" {EnumDishesNigth.Potato.GetEnumDescription( )}," +
                $" {EnumDishesNigth.Wine.GetEnumDescription( )}," +
                $" {EnumDishesNigth.Cake.GetEnumDescription( )}",
                dishResponse.Result
            );
        }

        [TestMethod( "Post order night 1, 2, 2, 4" )]
        public async Task PostOrderNightTest2( )
        {
            DishModelRequest dishModel = new DishModelRequest
            {
                Dishes = new List<int>( ) { 1, 2, 2, 4 }
            };

            var response = await m_Client.PostAsync(
                $"api/DishesType/{( int )EnumTimeDay.Night}",
                GetAsJsonContent( dishModel )
            );

            Xunit.Assert.True( response.IsSuccessStatusCode );
            DishModelResponse dishResponse = await GetAs<DishModelResponse>( response );

            Xunit.Assert.NotNull( dishResponse );
            Xunit.Assert.Equal(
                $"{EnumDishesNigth.Steak.GetEnumDescription( )}," +
                $" {EnumDishesNigth.Potato.GetEnumDescription( )}(X2)," +
                $" {EnumDishesNigth.Cake.GetEnumDescription( )}",
                dishResponse.Result
            );
        }

        [TestMethod( "Post order night 1, 2, 3, 5" )]
        public async Task PostOrderNightTest3( )
        {
            DishModelRequest dishModel = new DishModelRequest
            {
                Dishes = new List<int>( ) { 1, 2, 3, 5 }
            };

            var response = await m_Client.PostAsync(
                $"api/DishesType/{( int )EnumTimeDay.Night}",
                GetAsJsonContent( dishModel )
            );

            Xunit.Assert.True( response.IsSuccessStatusCode );
            DishModelResponse dishResponse = await GetAs<DishModelResponse>( response );

            Xunit.Assert.NotNull( dishResponse );
            Xunit.Assert.Equal(
                $"{EnumDishesNigth.Steak.GetEnumDescription( )}," +
                $" {EnumDishesNigth.Potato.GetEnumDescription( )}," +
                $" {EnumDishesNigth.Wine.GetEnumDescription( )}," +
                " Opção inválida",
                dishResponse.Result
            );
        }

        [TestMethod( "Post order night 1, 1, 2, 3, 5" )]
        public async Task PostOrderNightTest4( )
        {
            DishModelRequest dishModel = new DishModelRequest
            {
                Dishes = new List<int>( ) { 1, 1, 2, 3, 5 }
            };

            var response = await m_Client.PostAsync(
                $"api/DishesType/{( int )EnumTimeDay.Night}",
                GetAsJsonContent( dishModel )
            );

            Xunit.Assert.True( response.IsSuccessStatusCode );
            DishModelResponse dishResponse = await GetAs<DishModelResponse>( response );

            Xunit.Assert.NotNull( dishResponse );
            Xunit.Assert.Equal(
                $"{EnumDishesNigth.Steak.GetEnumDescription( )}," +
                " Não pode repetir o prato",
                dishResponse.Result
            );
        }

        [TestMethod( "Post order without dish model" )]
        public async Task PostOrderWithoutDishModel( )
        {
            DishModelRequest dishModel = new DishModelRequest { };

            var response = await m_Client.PostAsync(
                $"api/DishesType/{( int )EnumTimeDay.Night}",
                GetAsJsonContent( dishModel )
            );

            Xunit.Assert.Equal( HttpStatusCode.BadRequest, response.StatusCode );
        }
        #endregion
    }
}

/// <reference path="../_references.js" />


var storeApp = angular.module("storeApp", ['ngRoute']);
storeApp.config(function ($routeProvider) {
    $routeProvider.caseInsensitiveMatch = true;

    $routeProvider.when("/", {
        redirectTo:"/HomePage"
    }).when("/ProductList", {

    templateUrl: "../Templates/ProductTemplates/ProductList.html",
    controller: "productController",


    }).when("/Products/:id", {

        templateUrl: "../Templates/ProductTemplates/SingleProductDetails.html",
        controller: "productController"

    }).when("/HomePage", {

    templateUrl: "../Templates/ProductTemplates/HomePage.html",
    controller: "productController"

    }).otherwise({
        redirectTo: "/"
    });
});
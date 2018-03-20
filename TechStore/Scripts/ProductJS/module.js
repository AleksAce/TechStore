/// <reference path="../_references.js" />


var storeApp = angular.module("storeApp", ['ngRoute']);
storeApp.config(function ($routeProvider) {
    $routeProvider.caseInsensitiveMatch = true;

    $routeProvider.when("/", {
        redirectTo:"/Products"
    }).when("/Products", {

    templateUrl: "../Templates/ProductTemplates/ProductList.html",
    controller: "productController",


    }).when("/Products/:id", {

        templateUrl: "../Templates/ProductTemplates/SingleProductDetails.html",
        controller: "productController"

    }).when("/Product2", {

    templateUrl: "../Templates/ProductTemplates/Product2.html",
    controller: "product2Controller"

    }).otherwise({
        redirectTo: "/"
    });
});
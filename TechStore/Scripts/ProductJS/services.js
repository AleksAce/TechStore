storeApp.factory("productFactory", function ($http) {
    var factory = {};
    factory.getAllProducts = function () {
        return $http({
            method: "GET",
            url: "../api/Product"
        });
    };
    return factory;
});
storeApp.directive("productList", function (productFactory) {
    return {
        restrict: "E",
        templateUrl: "../Templates/ProductTemplates/AllProducts.html",
        link: function (scope, elem, attr) {
            productFactory.getAllProducts().then(function (response) {
                scope.products = response.data;
            });
        },
    }
});
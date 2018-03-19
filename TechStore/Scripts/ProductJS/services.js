storeApp.factory("productFactory", function ($http) {
    var factory = {};
    factory.getAllProducts = function (success, onerror) {
        return $http({
            method: "GET",
            url: "../api/Product"
        }).then(function (resp) {
            //on success
            success(resp);
        }, function (error) {
            //on error
            onerror(error.data.ExceptionMessage);
        });
    };
    return factory;
});
storeApp.directive("productList", function (productFactory) {
    return {
        restrict: "E",
        templateUrl: "../Templates/ProductTemplates/AllProducts.html",
        link: function (scope, elem, attr) {
            var onSuccess = function (response) {
                scope.products = response.data;
            };
            var onError = function (err) {
                console.log("ERROR: "+err);
            };
            productFactory.getAllProducts(onSuccess, onError);
        },
    }
});
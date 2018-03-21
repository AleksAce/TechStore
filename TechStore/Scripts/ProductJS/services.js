storeApp.factory("productFactory", function ($http) {
    var factory = {};
    //Things are loading, set this to false when loaded.
    factory.productsLoading = true;
    factory.getAllProducts = function () {
        factory.productsLoading = true;
        return  $http({
            method: "GET",
            url: "../api/Product"
        });
    };
    factory.getSingleProduct = function (id) {  
        factory.productsLoading = true;
        return $http({
            method: "GET",
            url: "../api/Product/" + id,
            
        });
    };
    return factory;
});

storeApp.directive("productDetails", function (productFactory, $routeParams) {
    return {
        restrict: "E",
        templateUrl: "../Templates/ProductTemplates/DirectiveTemplates/ProductDetails.html",
        link: function (scope, elem, attr) {
            //If not supplied by routeparams.. do the default first ID;
            productFactory.getSingleProduct($routeParams.id).then(
                function (response) {
                    scope.product = response.data;
                    productFactory.productsLoading = false;
                    scope.productsLoading = productFactory.productsLoading; 
                },
                function (error) {
                    console.log("ERROR: " + error);
                    productFactory.productsLoading = false;
                    scope.productsLoading = productFactory.productsLoading; 
                });
        },
    }
});
storeApp.directive("productList", function (productFactory) {
    return {
        restrict: "E",
        templateUrl: "../Templates/ProductTemplates/DirectiveTemplates/AllProducts.html",
        link: function (scope, elem, attr) {
            //If not supplied by routeparams.. do the default first ID;
            scope.productsLoading = productFactory.productsLoading; 
            productFactory.getAllProducts().then(
                function (response) {
                 
                    scope.products = response.data;
                    productFactory.productsLoading = false;
                    scope.productsLoading = productFactory.productsLoading; 
                },
                function (err) {
                    console.log("ERROR: " + err);
                    productFactory.productsLoading = false;
                    scope.productsLoading = productFactory.productsLoading; 
                });
        },
    }
});
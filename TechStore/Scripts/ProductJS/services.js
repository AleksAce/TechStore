storeApp.factory("productFactory", function ($http) {
    var factory = {};
    //Things are loading, set this to false when loaded.
    factory.productsLoading = true;
    factory.getAllProductsForMainPage = function () {
        factory.productsLoading = true;
        return $http({
            method: "Get",
            url: "../api/Product/GetForMainPage",
            params:{
                forMainPage: true,
            },
        });
    };
    factory.getAllProducts = function () {
        factory.productsLoading = true;
        return  $http({
            method: "Get",
            url: "../api/Product"
        });
    };
    factory.getProductChunk = function (index) {
        factory.productsLoading = true;
        return $http({
            method: "Get",
            url: "../api/Product/ProductChunk",
            params: {
                index: index,
                numItems: 2,
            },
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
storeApp.directive("mainPageProducts", function (productFactory) {
    return {
        restrict: "E",
        templateUrl: "../Templates/ProductTemplates/DirectiveTemplates/MainPageProducts.html",
        link: function (scope, elem, attr) {
            //If not supplied by routeparams.. do the default first ID;
            scope.productsLoading = productFactory.productsLoading;
            productFactory.getAllProductsForMainPage().then(
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
storeApp.directive("productList", function (productFactory) {
    return {
        restrict: "E",
        templateUrl: "../Templates/ProductTemplates/DirectiveTemplates/AllProducts.html",
        link: function (scope, elem, attr) {
            //If not supplied by routeparams.. do the default first ID;
            var indexOfProducts = attr.productindex;
            console.log(attr);
            scope.productsLoading = productFactory.productsLoading; 
            productFactory.getProductChunk(indexOfProducts).then(
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
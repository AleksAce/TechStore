storeApp.factory("productFactory", function ($http) {
    var factory = {};
    factory.getAllProductsForMainPage = function () {
        return $http({
            method: "Get",
            url: "../api/Product/GetForMainPage",
            params:{
                forMainPage: true,
            },
        });
    };
    factory.getAllProducts = function () {
        return  $http({
            method: "Get",
            url: "../api/Product"
        });
    };
    factory.getProductChunk = function (index) {
        return $http({
            method: "Get",
            url: "../api/Product/ProductChunk",
            params: {
                index: index,
                numItems: 10,
            },
        });
    };
    factory.getSingleProduct = function (id) {  
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
                },
                function (error) {
                    console.log("ERROR: " + error);
                });
        },
    }
});
storeApp.directive("mainPageProducts", function (productFactory) {
    return {
        restrict: "E",
        templateUrl: "../Templates/ProductTemplates/DirectiveTemplates/MainPageProducts.html",
        link: function (scope, elem, attr) {
            productFactory.getAllProductsForMainPage().then(
                function (response) {
                    scope.products = response.data;
                },
                function (err) {
                    console.log("ERROR: " + err);
                });
        },
    }
});
storeApp.directive("productList", function (productFactory) {
    return {
        restrict: "E",
        templateUrl: "../Templates/ProductTemplates/DirectiveTemplates/AllProducts.html",
        link: function (scope, elem, attr) {
            console.log(attr);
            //get the first chunk when instantiated
            productFactory.getProductChunk(0).then(
                function(response) {
                    if (response.data.length == 0) {
                        return;
                    }
                    scope.products = response.data;
                },
                function(err) {
                    console.log("ERROR: " + err);
                });


        },
        controller: function ($scope) {
            //start from 1 since the first one is initial and is 0 when linked
            var chunkNumber = 1;
            $(document).on("wheel", function () {
                var sentinel = document.body.offsetHeight - 1080;
                if (window.innerHeight + window.scrollY >= sentinel) {
                      productFactory.getProductChunk(chunkNumber).then(
                          function (response) {
                              if (response.data.length == 0) {
                                  return;
                              }
                              $scope.products.push.apply($scope.products,response.data);
                              chunkNumber++;
                          },
                          function (err) {
                              console.log("ERROR: " + err);
                          });
                }
            });

        }

}
});
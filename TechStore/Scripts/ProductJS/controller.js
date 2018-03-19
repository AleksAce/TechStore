

storeApp.controller("productController", function ($scope,$http) {

    $scope.message = "Hello";
    $scope.productsLoading = true;
    $scope.lul = "lul";
});

storeApp.controller("product2Controller", function ($scope) {
    $scope.message = "Hello";
    $scope.product = "product2";

});
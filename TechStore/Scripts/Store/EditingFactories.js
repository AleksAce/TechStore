var htmlAppendingFactory = function () {
        var fact = {};
        fact.htmlToAppendForRemoveProduct = function (ProductID, Name) {
            return "<div class='col-md-6' id='" + ProductID +
                "'><p>" + Name + "</p>" +
                "</div>" +
                "<div class='col-md-6'>" +
                "<button class='btn btn-danger btn-remove' onclick='RemoveProduct(" + ProductID + ",event)'" +
                ">Remove Product</button>" +
                "</div>";
        };
        fact.htmlToAppendForAddProduct = function (ProductID, Name) {
            return "<div class='col-md-6' id='" + ProductID +
                "'><p>" + Name + "</p>" +
                "</div>" +
                "<div class='col-md-6'>" +
                "<button class='btn btn-success' onclick='AddProduct(" + ProductID + ", event)'" +
                ">Add Product</button>" +
                "</div>";
        };
        fact.htmlToAppendForRemoveCategories = function (CategoryID, Name) {
            return "<div class='col-md-6' id='" + CategoryID +
                "'><p>" + Name + "</p>" +
                "</div>" +
                "<div class='col-md-6'>" +
                "<button class='btn btn-danger btn-remove' onclick='RemoveCategory(" + CategoryID + ", event)'" +
                ">Remove From Category</button>" +
                "</div>";
        };
        fact.htmlToAppendForAddCategories = function (CategoryID, Name) {
            return "<div class='col-md-6' id='" + CategoryID +
                "'><p>" + Name + "</p>" +
                "</div>" +
                "<div class='col-md-6'>" +
                "<button class='btn btn-success' onclick='AddCategory(" + CategoryID + ", event)'" +
                ">Add To Category</button>" +
                "</div>";
        };
       

        return fact;
};

var storeFactory = function (appendingFactory) {
    var fact = {};

    fact.GetProductsNotInOrder = function (divToAppendTo,orderID) {
        //Get all products
        divToAppendTo.empty();
        $.ajax({
            type: "get",
            url: "/Order/ProductsNotInOrder/" + orderID,
        success: function (data) {
            //Once you get the data append it to the Div

            $.each(data, function (index, item) {
                //Append with add button
                let productHTML = appendingFactory.htmlToAppendForAddProduct(item.ProductID, item.ProductName);
                divToAppendTo.append(productHTML);
            });
        },
        data: {
            orderID: orderID,
        },
        contentType: "application/json; charset=utf-8",
            dataType: "json",
        });
    };
    fact.GetProductsPerOrder = function (divToAppendTo,orderID) {
        //Get all products
        divToAppendTo.empty();
        $.ajax({
            type: "get",
            url: "/Order/ProductsPerOrder/" + orderID,
        success: function (data) {
            //Once you get the data append it to the Div

            $.each(data, function (index, item) {
                //Append with remove button
                let productHTML = appendingFactory.htmlToAppendForRemoveProduct(item.ProductID, item.ProductName);
                divToAppendTo.append(productHTML);
            });
        },
        data: {
            orderID: orderID,
        },
        contentType: "application/json; charset=utf-8",
            dataType: "json",
        });
    };

    fact.GetCategoriesNotInProduct = function (divToAppendTo,productID) {
        //Get all products
        divToAppendTo.empty();
        $.ajax({
            type: "get",
            url: "/Product/CategoriesNotInProduct/" + productID,
        success: function (data) {
            //Once you get the data append it to the Div

            $.each(data, function (index, item) {
                //Append with add button
                let categoryHTML = appendingFactory.htmlToAppendForAddCategories(item.CategoryID, item.CategoryName);
                divToAppendTo.append(categoryHTML);
            });
        },
        data: {
            productID: productID,
        },
        contentType: "application/json; charset=utf-8",
            dataType: "json",
                });
    };
    fact.GetCategoriesPerProduct = function (divToAppendTo,productID) {
        //Get all products
        divToAppendTo.empty();
        $.ajax({
            type: "get",
            url: "/Product/CategoriesPerProduct/" + productID,
        success: function (data) {
            //Once you get the data append it to the Div
    
            $.each(data, function (index, item) {
                //Append with remove button
                let categoryHTML = appendingFactory.htmlToAppendForRemoveCategories(item.CategoryID, item.CategoryName);
                divToAppendTo.append(categoryHTML);
            });
        },
        data: {
            productID: productID,
        },
        contentType: "application/json; charset=utf-8",
            dataType: "json",
                    });
    };
    fact.GetProductsNotInCategory = function (divToAppendTo,categoryID) {
        //Get all products
        divToAppendTo.empty();
        $.ajax({
            type: "get",
            url: "/Category/ProductsNotInCategory/" + categoryID,
            success: function (data) {
                //Once you get the data append it to the Div

                $.each(data, function (index, item) {
                    //Append with add button
                    let productHTML = appendingFactory.htmlToAppendForAddProduct(item.ProductID, item.ProductName);
                    divToAppendTo.append(productHTML);
                });
            },
            data: {
                categoryID: categoryID,
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });
    };
    fact.GetProductsPerCategory = function (divToAppendTo,categoryID) {
        //Get all products
        divToAppendTo.empty();
        $.ajax({
            type: "get",
            url: "/Category/ProductsPerCategory/" + categoryID,
            success: function (data) {
                //Once you get the data append it to the Div

                $.each(data, function (index, item) {
                    //Append with remove button
                    let productHTML = appendingFactory.htmlToAppendForRemoveProduct(item.ProductID, item.ProductName);
                    divToAppendTo.append(productHTML);
                });
            },
            data: {
                categoryID: categoryID,
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });
    };

    return fact;
}

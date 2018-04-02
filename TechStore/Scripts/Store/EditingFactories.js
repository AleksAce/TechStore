var htmlAppendingFactory = function () {
    var fact = {};
    fact.htmlToAppendForRemoveOrder = function (OrderID, Name, fullPrice) {
        return "<div class='col-md-6' id='" + OrderID +
            "'>" + "<a class='appended-buttons btn btn-primary id='"
            + OrderID + "' href='/Order/Details/" +
            OrderID + "'>" + " $" + fullPrice + " " + Name + "</a>" +
            "</div>" +
            "<div class='col-md-6'>" +
            "<button class='btn btn-danger btn-remove' onclick='RemoveOrder(" + OrderID + ",event)'" +
            ">Remove Order</button>" +
            "</div>";
    };
        fact.htmlToAppendForRemoveProduct = function (ProductInfoID, Name, Price) {
            return "<div class='col-md-6' id='" + ProductInfoID +
                "'><p>" + " $" + Price + " " + Name + "</p>" + 
                "</div>" +
                "<div class='col-md-6'>" +
                "<button class='btn btn-danger btn-remove' onclick='RemoveProduct(" + ProductInfoID + ",event)'" +
                ">Remove Product</button>" +
                "</div>";
        };
        fact.htmlToAppendForAddProduct = function (ProductID, Name, Price) {
            return "<div class='col-md-6' id='" + ProductID +
                "'><p>" + " $" + Price + " " + Name + "</p>" +
                
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
    fact.GetOrdersPerCustomer = function (divToAppendTo, customerID) {
        //Get all products
        divToAppendTo.empty();
        $.ajax({
            type: "get",
            url: "/Customer/GetOrders/" + customerID,
            success: function (data) {
                //Once you get the data append it to the Div

                $.each(data, function (index, item) {
                    //Append with remove button
                    let itemHTML = appendingFactory.htmlToAppendForRemoveOrder(item.OrderID, item.OrderDate, item.FullPrice);
                    divToAppendTo.append(itemHTML);
                });
            },
            data: {
                customerID: customerID,
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });
    };
    fact.GetAllAvailableProducts = function (divToAppendTo) {
        //Get all products
        divToAppendTo.empty();
        $.ajax({
            type: "get",
            url: "/Order/GetAllProducts" ,
            success: function (data) {
                //Once you get the data append it to the Div

                $.each(data, function (index, item) {
                    //Append with add button
                    let productHTML = appendingFactory.htmlToAppendForAddProduct(item.ProductID, item.ProductName, item.Price);
                    divToAppendTo.append(productHTML);
                });
            },
            data: {
                
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
                let productHTML = appendingFactory.htmlToAppendForRemoveProduct(item.ProductInfoID, item.ProductName, item.Price);
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

/// <reference path="angular.js" />

var app = angular.module("myApp", []);

app.controller("ProductCtrl", function ($scope) {
    $scope.urunler = [];

    function init() {
        var data = Json.parse(localStorage.getItem("urunler"));
        $scope.urunler = data === null ? [] : data;
    }

    $scope.ekle = function () {

        $scope.urunler.push({
            // id: guid();hocanın koduncan bak burada özel guid üreten bir kod eklenicek.
            urunAdi: $scope.yeni.urunAdi,
            fiyat: $scope.yeni.fiyat,
            eklenmeZamani: new Date()
        });
        $scope.yeni.urunAdi = "";
        $scope.yeni.fiyat = "";
        //local storage eklemek için geçiveri tabanı gibi göstermelik.
        localStorage.setItem("urunler", JSON.stringify($scope.urunler));
    };
    init();
});
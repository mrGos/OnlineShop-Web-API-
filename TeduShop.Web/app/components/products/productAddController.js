(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function productAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }


        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }
        $scope.AddProduct = AddProduct;

        function AddProduct() {
            if ($scope.product.Price > 0 && $scope.product.OriginalPrice > 0) {
                $scope.product.MoreImages = JSON.stringify($scope.moreImages);
                apiService.post('/api/product/create', $scope.product, function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('products');
                }, function (error) {
                    notificationService.displayError(result.data.Name + ' thêm mới không thành công.');
                });
            } else {
                notificationService.displayError('thêm mới không thành công');
            }

        }

        function loadProductCategory() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function (error) {
                notificationService.displayError('Lấy loại sản phẩm thất bại.');
            });
        }

        $scope.ChooseImage = ChooseImage;
        function ChooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
            }
            finder.popup();
        }


        $scope.moreImages = [];
        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    if ($scope.moreImages.indexOf(fileUrl) <= -1) {
                        $scope.moreImages.push(fileUrl);
                    }
                })
            }
            finder.popup();
        }

        loadProductCategory();
    }

})(angular.module('shop.products'));




(function () {
    'use strict';
    angular.module('app').controller('productsCtrl', productsCtrl);

    productsCtrl.$inject = ['$http'];

    function productsCtrl($http) {
        var vm = this;
        vm.page = {
            order: 'Title',
            sort: 'ASC',
            skip: '1',
            take: '10'
        }
        vm.pageIsLoading = false;
        vm.products = [];
        vm.count = 0;
        vm.orderBy = orderBy;
        vm.loadData = loadData;

        loadData();

        function loadData() {
            vm.pageIsLoading = true;

            var url = 'api/products/' + vm.page.order + '/' + vm.page.sort + '/' + vm.page.skip + '/' + vm.page.take;
            $http.get(url)
                .then(success)
                .catch(error)
                .finally(end);

            function success(result) { 
                vm.products = result.data.Products;
                vm.count = result.data.Count;
            }

            function error(result) { 
                console.log(result);
            }

            function end() {
                vm.pageIsLoading = false;
            }
        }

        function orderBy(item) {
            if (vm.page.sort == 'ASC') {
                vm.page.sort = 'DESC'
            } else if (vm.page.sort == 'DESC') {
                vm.page.sort = 'ASC'
            }

            vm.page.order = item;
            loadData();
        }
    }
})();
/// <reference path="../model.ts" />

module AutomatedTesting {
    'use strict';

    interface AccountsViewModel extends ng.IScope {
        accounts: Account[];
        client: Client;
        totalBalance: number;
    }

    class AccountsController {

        private baseUrl: string;

        public static $inject = [
            "$log",
            "$scope",
            "AccountsService"
        ];

        constructor(
            private $log: ng.ILogService,
            private $scope: AccountsViewModel,
            private accountsService: IAccountsService
            ) {

            $scope.accounts = [];

            $scope.$watch('client', () => {

                $scope.accounts.length = 0;

                if ($scope.client) {
                    accountsService.getClientAccounts($scope.client.Id)
                        .then(accounts => {
                            $log.debug('got accounts', accounts);
                            angular.copy(accounts, $scope.accounts);
                        });

                    accountsService.getClientTotalAccountBalance($scope.client.Id)
                        .then(totalBalance=> {
                            $log.debug('got total balance', totalBalance);
                            $scope.totalBalance = totalBalance;
                        });
                }

            });

        }
    }


    angular.module('AutomatedTestingApp')
        .controller('AccountsController', AccountsController);
}
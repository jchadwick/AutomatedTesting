/// <reference path="../model.ts" />

module AutomatedTesting {
    'use strict';

    interface ClientsViewModel extends ng.IScope {
        clients: Client[];
    }

    class ClientsController {

        private baseUrl: string;

        public static $inject = [
            "$log",
            "$scope",
            "ClientsService"
        ];

        constructor(
            private $log: ng.ILogService,
            private $scope: ClientsViewModel,
            private clientsService: IClientsService
            ) {

            $scope.clients = [];

            clientsService.getClients()
                .then(clients => {
                    $log.debug('got Clients', clients);
                    angular.copy(clients, $scope.clients);
                });
        }
    }


    angular.module('AutomatedTestingApp')
        .controller('ClientsController', ClientsController);
}

var ViewModel = function () {
    var self = this;
    self.fileInput = ko.observable();
    self.card = ko.observableArray();
    self.deck = ko.observableArray();
    self.detail = ko.observable();
    self.deckDetail = ko.observable();
    self.error = ko.observable();

    //New Card model
    self.newCard = {
        Id: ko.observable(),
        Color: ko.observable(),
        Category: ko.observable(),
        CardNumber: ko.observable(),
        Subject: ko.observable(),
        SpecificSubject: ko.observable(),
        Author: ko.observable(),
        Facts: ko.observable(),
        Question: ko.observable(),
        Answer: ko.observable(),
        fileInput: ko.observable()
    }

    //New Deck model
    self.newDeck = {
        DeckName: ko.observable()
    }

    var cardsUri = './../api/cards/';
    var decksUri = './../api/decks1/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllCards() {
        ajaxHelper(cardsUri, 'GET').done(function (data) {
            self.card(data);
        });
    }
    function getAllDecks() {
        ajaxHelper(decksUri, 'GET').done(function (data) {
            self.deck(data);
        });
    }

    //Add Card
    self.addCard = function (formElement) {
        var card = {
            Id: self.newCard.Id(),
            Color: self.newCard.Color(),
            Category: self.newCard.Category(),
            CardNumber: self.newCard.CardNumber(),
            Subject: self.newCard.Subject(),
            SpecificSubject: self.newCard.SpecificSubject(),
            Author: self.newCard.Author(),
            Facts: self.newCard.Facts(),
            Question: self.newCard.Question(),
            Answer: self.newCard.Answer(),
            fileInput: self.newCard.fileInput()
        };

        ajaxHelper(cardsUri, 'POST', card).done(function (item) {
            self.card.push(item);
        });
    }

    //Add Deck
    self.addDeck = function (formElement) {
        var deck = {
            DeckName: self.newDeck.DeckName()
        };

        ajaxHelper(decksUri, 'POST', deck).done(function (item) {
            self.deck.push(item);
        });
    }

    //Update Card  
    self.updateCard = function (card) {
        ajaxHelper(cardsUri + self.card.Id, 'PUT', card).done(function (data) {
            alert('Card Updated Successfully !');
            getAllCards();
            self.cancel();
        });
    }
    // Get Card Detail
    self.getCardDetail = function (item) {
        ajaxHelper(cardsUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    // Get Deck Detail
    self.getDeckDetail = function (item) {
        ajaxHelper(decksUri + item.Id, 'GET').done(function (data) {
            self.deckDetail(data);
        });
    }

    //Delete Card  
    self.deleteCard = function (item) {

        ajaxHelper(cardsUri + item.Id, 'DELETE').done(function () {
            alert('Card Deleted Successfully');
            getAllCards();
        })

    }


    // Fetch the initial data.
    getAllCards();
    getAllDecks();
};

ko.applyBindings(new ViewModel());

function CallPrint(strid) {
    var prtContent = document.getElementById(strid);
    var WinPrint = window.open('', '_blank', 'letf=0,top=0,width=800,height=500,toolbar=0,scrollbars=0,status=0,dir=ltr');
    WinPrint.document.write(prtContent.innerHTML);
    WinPrint.document.close();
    WinPrint.focus();
    WinPrint.print();
    WinPrint.close();
    //prtContent.innerHTML = strOldOne;
}
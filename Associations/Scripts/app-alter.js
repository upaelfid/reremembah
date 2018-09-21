
var ViewModel = function (){ //Make the self as 'this' reference
    var self = this;
    //Declare observable which will be bind with UI 
    self.Id = ko.observable();
    self.DeckId = ko.observable();
    self.Color = ko.observable()
    self.Category = ko.observable();
    self.CardNumber = ko.observable();
    self.Subject = ko.observable();
    self.SpecificSubject = ko.observable();
    self.Author = ko.observable();
    self.Facts = ko.observable();
    self.Question = ko.observable();
    self.Answer = ko.observable();
    self.fileInput = ko.observable();

    //The Object which stored data entered in the observables
    var cardData = {
        Id: self.Id,
        DeckId: self.DeckId,
        Color: self.Color,
        Category: self.Category,
        CardNumber: self.CardNumber,
        Subject: self.Subject,
        SpecificSubject: self.SpecificSubject,
        Author: self.Author,
        Facts: self.Facts,
        Question: self.Question,
        Answer: self.Answer,
        fileInput: self.fileInput
    };

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

    //Declare an ObservableArray for Storing the JSON Response
    self.Cards = ko.observableArray();



    var cardsUri = './../../api/cards/';

    //Function to perform POST (insert Employee) operation
    self.save = function () {
        //Ajax call to Insert the Employee
        $.ajax({
            type: "POST",
            url: cardsUri,
            data: ko.toJSON(cardData), //Convert the Observable Data into JSON
            contentType: "application/json",
            success: function (data) {
                alert("Record Added Successfully");

                GetCards();
            },
            error: function () {
                alert("Failed");
            }
        });
        //Ends Here
    };

    self.update = function () {
        var url = cardsUri + self.Id();
        //alert(url);
        $.ajax({
            type: "PUT",
            url: url,
            data: ko.toJSON(cardData),
            contentType: "application/json",
            success: function (data) {
                alert("Record Updated Successfully");
                GetCards();
            },
            error: function (error) {
                alert(error.status + "<!----!>" + error.statusText);
            }
        });
    };

    //Function to perform DELETE Operation
    self.deleterec = function (card) {
        $.ajax({
            type: "DELETE",
            url: cardsUri + card.Id,
            success: function (data) {
                alert("Record Deleted Successfully");
                GetCards();//Refresh the Table
            },
            error: function (error) {
                alert(error.status + "<--and--> " + error.statusText);
            }
        });
        // alert("Clicked" + employee.EmpNo)
    };

    //Function to Read All Employees
    function GetCards() {
        //Ajax Call Get All Employee Records
        $.ajax({
            type: "GET",
            url: cardsUri,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                self.Cards(data); //Put the response in ObservableArray
            },
            error: function (error) {
                alert(error.status + "<--and-is-> " + error.statusText);
            }
        });
        //Ends Here
    }

    //Function to Read All Employees
    function GetCardsByDeck(DeckID) {
        //Ajax Call Get All Employee Records
        $.ajax({
            type: "GET",
            url: cardsUri,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {              
                var p = [];
                for (i = 0; i < data.length; i++) {
                    var s = data[i].DeckId;
                    if (s == DeckID) {
                        p.push(data[i]);
                    };

                }
                return self.Cards(p); //Put the response in ObservableArray
            },
            error: function (error) {
                alert(error.status + "<--and-is-> " + error.statusText);
            }
        });
        //Ends Here
    }

    self.getid = function(cardnumber) {
            $("#card-"+cardnumber).flip({
                axis: "y", // y or x
                reverse: false, // true and false
                trigger: "click", // click or hover
                speed: 300
            });
    }

    self.getCategory = function () {

    }

    //Function to Display record to be updated
    self.getselectedcard = function (card) {
        self.Id(card.Id),
        self.DeckId(card.DeckId),
        self.Color(card.Color),
        self.Category(card.Category),
        self.CardNumber(card.CardNumber),
        self.Subject(card.Subject),
        self.SpecificSubject(card.SpecificSubject),
        self.Author(card.Author),
        self.Facts(card.Facts),
        self.Question(card.Question),
        self.Answer(card.Answer),
        self.fileInput(card.fileInput)
    };

    //GetCards();
    GetCardsByDeck(window.location.pathname.split('/')[3]);
};

ko.applyBindings(new ViewModel());


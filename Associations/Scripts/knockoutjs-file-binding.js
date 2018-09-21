﻿//knockoujs-file-binding
(function () {
    var factory = function (ko) {

        ko.bindingHandlers['file'] = {
            init: function (element, valueAccessor, allBindings) {
                var fileContents, fileName, allowed, prohibited, reader;

                if ((typeof valueAccessor()) === "function") {
                    fileContents = valueAccessor();
                } else {
                    fileContents = valueAccessor()['data'];
                    fileName = valueAccessor()['name'];

                    allowed = valueAccessor()['allowed'];
                    if ((typeof allowed) === 'string') {
                        allowed = [allowed];
                    }

                    prohibited = valueAccessor()['prohibited'];
                    if ((typeof prohibited) === 'string') {
                        prohibited = [prohibited];
                    }

                    reader = (valueAccessor()['reader']);
                }

                reader || (reader = new FileReader());
                reader.onloadend = function () {
                    fileContents(reader.result);
                }

                var handler = function () {
                    var file = element.files[0];

                    // Opening the file picker then canceling will trigger a 'change'
                    // event without actually picking a file.
                    if (file === undefined) {
                        fileContents(null)
                        return;
                    }

                    if (allowed) {
                        if (!allowed.some(function (type) { return type === file.type })) {
                            console.log("File " + file.name + " is not an allowed type, ignoring.")
                            fileContents(null)
                            return;
                        }
                    }

                    if (prohibited) {
                        if (prohibited.some(function (type) { return type === file.type })) {
                            console.log("File " + file.name + " is a prohibited type, ignoring.")
                            fileContents(null)
                            return;
                        }
                    }

                    reader.readAsDataURL(file); // A callback (above) will set fileContents
                    if (typeof fileName === "function") {
                        fileName(file.name)
                    }
                }

                ko.utils.registerEventHandler(element, 'change', handler);
            }
        }
    }

    if ((typeof define === 'function') && define.amd) {
        define(['knockout'], factory);
    } else {
        factory(ko);
    }
})();

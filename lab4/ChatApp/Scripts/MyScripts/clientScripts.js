$(function () {
    $('#chatBody').hide();
    $('#loginBlock').show();
    $('#displayname').focus();
    
    $('#chatusers').hide();

    // Reference the auto-generated proxy for the hub.
    var chat = $.connection.chatHub;

    // Called when a new user connected
    chat.client.onConnected = function (userName, allUsers) {

        $('#loginBlock').hide();
        $('#chatBody').show();
        $('#header').html('<h2>' + userName + ', welcome to the chat</h2>');

        // Add all users to list
        for (i = 0; i < allUsers.length; i++) {
            AddUser(allUsers[i].Name);
        }
        
        chat.server.getAllMessages($('#displayname').val(), getSelectedUser());
    }

    // Add connected user to the list of users
    chat.client.onNewUserConnected = function (name) {
        AddUser(name);
    }

    // Delete user from lists
    chat.client.onUserDisconnected = function (userName) {
        $('#user' + userName).remove();
        $("#userdropdown").find('option[value="dd' + userName + '"]').remove();
    }

    // Function that the hub can call back to display message
    chat.client.getNewMessage = function (from, to, message, time, photo) {
        if (from == $('#displayname').val()) {
            addMessageToPage(from, message, time, photo);
        }
        else {
            if ((from == getSelectedUser() && to == $('#displayname').val()) || (to == "GroupChat" && getSelectedUser() == "GroupChat")) {
                addMessageToPage(from, message, time, photo);
            }
        }
        
    };

    // Load old messages with user to page
    chat.client.loadMessages = function (messages) {
        for (i = 0; i < messages.length; i++) {
            addMessageToPage(messages[i].FromUser, messages[i].MessageText, messages[i].Time, messages[i].Photo);
        }
    }

    chat.client.showAlert = function (text) {
        alert(text);
    }


    // Start the connection
    $.connection.hub.start().done(function () {
        // Login processing
        $("#btnLogin").click(function () {
            var username = $("#displayname").val();
            if (username.length > 0) {
                chat.server.connect(username);
            }
            else {
                alert("Please enter a valid name! Name can't be empty.");
            }
        });

        // get messages with chosen user from server
        $("#userdropdown").change(function () {
            //clear message box and load historical messages
            $('#discussion').html("");
            chat.server.getAllMessages($('#displayname').val(), getSelectedUser());
        });

        // Send message to server
        $('#sendmessage').click(function () {
            var image = $('#imgTest').attr('src');
            
            if ($('#message').val() != '' || image) {
                // Call the Send method on the hub
                chat.server.send($('#displayname').val(), getSelectedUser(), $('#message').val(), getTimeString(), (image) ? image : null);
                // Clear text box and reset focus for next message
                $('#message').val('').focus();
                $('#imgTest').removeAttr('src');
            }
        });

        // load image into temp object
        $('#fileinput').change(function (e) {
            var that = $(this);
            var filesSelected = that[0].files;
            if (filesSelected.length > 0) {
                var file = filesSelected[0];
                var readers = new FileReader();
                readers.onload = function () {
                    $("#imgTest").attr('src', readers.result);
                }
                readers.readAsDataURL(file);
            }
        });
    });
});


// Add an user to lists
function AddUser(name) {
    $("#chatusers").append('<p id="user' + name + '">' + name + '</p>');
    $("#userdropdown").append('<option value="dd' + name + '">' + name + '</option>');
};

// Add a message to container
function addMessageToPage(name, message, time, photo) {
    $('#discussion').append('<p><strong>' + htmlEncode(name)
        + '</strong> ' + time + ' : ' + htmlEncode(message) + '</p>');
    
    // process photo
    if (photo) {
        var img = new Image();
        img.src = photo;
        $('#discussion').append(img);
    }

    // scroll to bottom
    $('#discussion').stop().animate({
        scrollTop: $('#discussion')[0].scrollHeight
    }, 800);
};

// Chosen user for chatting
function getSelectedUser() {
    return $("#userdropdown").children("option:selected").text();
};

// This optional function html-encodes messages for display in the page
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
};

function getTimeString() {
    var currentTime = new Date();
    return currentTime.toLocaleString();//toTimeString();
};
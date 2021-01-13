var arr;
var selectedItem;

function addUserClick() {
    if (selectedItem != undefined) {
        selectedItem.username = document.getElementById('inptName').value;
        selectedItem.userage = document.getElementById('inptAge').value;

        $.post('/updateuser', selectedItem, getUsers);

        selectedItem = null;
        document.getElementById('formSubmit').value = 'Add';
    } else {
        const name = $('.name').val();
        const age = $('.age').val();
        $('.name').val("");
        $('.age').val("");
        addUser(name, age);
    }
}

function getUsers() {
    $.get('/getusers', data => {
        createTable('table', data);
    });
}

function createTable(element, mas) {
    arr = mas;

    const div = document.getElementById(element);
    $(div).empty();

    const table = document.createElement("table");

    $(table).appendTo(div);
    $(table)
        .addClass("table table-bordered table-primary col-6");

    for (let i = 0; i < mas.length; i++) {
        $('<tr>').addClass('tr').appendTo(table);
        for (const key in mas[i]) {
            if (key === '_id') continue;

            $('<td>').addClass('td')
                .appendTo('.tr:last').text(mas[i][key]);
        }

        $('<td>').appendTo('.tr:last');
        $('<button>').text('Update').addClass('btn btn-warning text-light')
            .appendTo('td:last').click(() => {
                updateUser(mas[i]['_id']);
            });


        $('<td>').appendTo('.tr:last');
        $('<button>').text('Delete').addClass('btn btn-danger')
            .appendTo('td:last').click(() => {
                deleteUser((mas[i]['_id']));
            });
    }
}

function addUser(name, age) {
    if (!name || !age) return;

    $.post('/adduser', {
        username: name,
        userage: age
    }, () => {
        getUsers();
    });
}

function deleteUser(id) {
    $.post('/deleteuser', { id: id }, () => {
        getUsers();
    });
}

function updateUser(id) {
    const res = arr.find(x => x._id === id);

    if (res != undefined) {
        selectedItem = res;

        document.getElementById('inptName').value = selectedItem.username;
        document.getElementById('inptAge').value = selectedItem.userage;

        document.getElementById('formSubmit').value = 'Update';
    }

    $('.add').click(() => {
        $('.name').val("");
        $('.age').val("");
    });
}

$(document).ready(getUsers);

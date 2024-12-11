$(document).ready(function () {
    const avatarFormControlInput = document.getElementById('avatar-form-control');
    const updateAvatarSubmitButton = document.getElementById('update-avatar-submit');
    const updateNameSubmitButton = document.getElementById('btn-name-change');
    const userNameDisplay = document.getElementById('data-username'); 

    let isEditing = false; 

    if (updateNameSubmitButton) {
        updateNameSubmitButton.onclick = function (event) {
            event.preventDefault();

            if (!isEditing) {
                const currentName = userNameDisplay.textContent.trim();
                const inputField = document.createElement('input');
                inputField.type = 'text';
                inputField.value = currentName;
                inputField.className = 'form-control';
                inputField.id = 'name-change-input';

                userNameDisplay.replaceWith(inputField);
                updateNameSubmitButton.textContent = 'OK';
                isEditing = true;
            } else {
                const inputField = document.getElementById('name-change-input');
                const newName = inputField.value.trim();

                if (!newName) {
                    alert('Имя не может быть пустым. Пожалуйста, введите новое имя.');
                    return;
                }

                $.ajax({
                    url: '/user/username',
                    type: 'POST',
                    data: { name: newName },
                    success: function (response) {
                        const newDisplayName = document.createElement('p');
                        newDisplayName.textContent = newName;
                        newDisplayName.setAttribute('data-username', '');

                        inputField.replaceWith(newDisplayName);
                        isEditing = false;
                        location.reload();
                    },
                });
            }
        };
    }
});
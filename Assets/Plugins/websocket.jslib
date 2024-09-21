mergeInto(LibraryManager.library, {
    FetchJsonData: function(url) {
        window.alert("0");
        const requestOptions = {
            methods: 'GET', // ���������, ��� ����� ������������� ������ �������
            headers: {
                'Origin': 'http://localhost:50999',
                'Content-Type': 'application/json',
                // ��� �������������, �������� ���������, �������� ��� �����������
                // 'Authorization': 'Bearer YOUR_ACCESS_TOKEN'
            },
            mode: 'no-cors' // ������� ����� ��� ��������� CORS
        };
        window.alert("1");
        fetch(Pointer_stringify(url), requestOptions)
            .then(response => {
                if (!response.ok) throw new Error('Network response was not ok');
                return response.json();
            })
            .then(data => {
                const jsonString = JSON.stringify(data);
                const buffer = Module._malloc(jsonString.length + 1);
                Module.stringToUTF8(jsonString, buffer, jsonString.length + 1);
                window.alert("2");
                window.unityInstance.SendMessage('TimeRequesterJson', 'SetJSON', buffer);
                Module._free(buffer);
            })
            .catch(error => console.error('Error fetching JSON:', error));
    },

    Hello: function () {
        window.alert("Hello, world!");
    }
});
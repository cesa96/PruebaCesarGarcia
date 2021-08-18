const uri = 'api/Facturas';
let facturas = [];

function getFacturas() {
    fetch(uri)
        .then(response => response.json())

        .then(data => _displayFacturas(data))
        .catch(error => console.error('No se pueden obtener las factruas', error));
}

function addFactura() {
    const addNumeroTextbox = document.getElementById('add-numero');

    const factura = {
        numeroFactura: addNumeroTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(factura)
    })
        .then(response => response.json())
        .then(() => {
            getFacturas();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add factura.', error));
}

function deleteFactura(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getFacturas())
        .catch(error => console.error('Unable to delete factura.', error));
}

function displayEditForm(id) {
    const factura = facturas.find(factura => factura.idFactura === id);

    document.getElementById('edit-numeroFactura').value = factura.numeroFactura;
    document.getElementById('edit-idFactura').value = factura.idFactura;
    document.getElementById('edit-fechaFactura').value = factura.fechaFactura;
    document.getElementById('edit-tipoPago').value = factura.tipoPago;
    document.getElementById('edit-documentoCliente').value = factura.documentoCliente;
    document.getElementById('edit-nombreCliente').value = factura.nombreCliente;
    document.getElementById('edit-subTotal').value = factura.subTotal;
    document.getElementById('edit-descuento').value = factura.descuento;
    document.getElementById('edit-iva').value = factura.iva;
    document.getElementById('edit-totalDescuento').value = factura.totalDescuento;
    document.getElementById('edit-totalImpuesto').value = factura.totalImpuesto;
    document.getElementById('edit-total').value = factura.total;
    document.getElementById('editForm').style.display = 'block';
}

function updateFactura() {
    const facturaId = document.getElementById('edit-idFactura').value;
    const factura = {
        idFactura: parseInt(facturaId, 10),
        numeroFactura: document.getElementById('edit-numeroFactura').value.trim(),
        fechaFactura: document.getElementById('edit-fechaFactura').value.trim(),
        tipoPago: document.getElementById('edit-tipoPago').value.trim(),
        documentoCliente: document.getElementById('edit-documentoCliente').value.trim(),
        nombreCliente: document.getElementById('edit-nombreCliente').value.trim(),
        subTotal: document.getElementById('edit-subTotal').value.trim(),
        descuento: document.getElementById('edit-descuento').value.trim(),
        iva: document.getElementById('edit-iva').value.trim(),
        totaDescuento: document.getElementById('edit-totaDescuento').value.trim(),
        totalImpuesto: document.getElementById('edit-totalImpuesto').value.trim(),
        total: document.getElementById('edit-total').value.trim()
    };

    fetch(`${uri}/${facturaId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(factura)
    })
        .then(() => getFacturas())
        .catch(error => console.error('Unable to update factura.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(facturaCount) {
    const name = (facturaCount === 1) ? 'factura' : 'facturas';

    document.getElementById('counter').innerText = `${facturaCount} ${name}`;
}

function _displayFacturas(data) {
    const tBody = document.getElementById('facturas');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(factura => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${factura.idFactura})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteFactura(${factura.idFactura})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(factura.numeroFactura);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNode2 = document.createTextNode(factura.nombreCliente);
        td2.appendChild(textNode2);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    facturas = data;
}
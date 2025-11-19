const apiBase = 'https://localhost:57748/api/contacts';

async function fetchAll() {
    const res = await axios.get(apiBase);
    return res.data;
}

function clearErrors() {
    ['errFullName','errEmail','errPhone'].forEach(id => {
        const el = document.getElementById(id);
        if (el) el.innerText = '';
    });
}

function validateClient(contact) {
    clearErrors();
    let ok = true;
    if (!contact.FullName || contact.FullName.trim().length === 0) {
        document.getElementById('errFullName').innerText = 'Full name is required';
        ok = false;
    }
    if (!contact.Email || !/^[^@\s]+@[^@\s]+\.[^@\s]+$/.test(contact.Email)) {
        document.getElementById('errEmail').innerText = 'Valid email is required';
        ok = false;
    }
    if (contact.Phone && !/^\d+$/.test(contact.Phone)) {
        document.getElementById('errPhone').innerText = 'Phone must be numeric';
        ok = false;
    }
    return ok;
}

function readForm() {
    return {
        Id: parseInt(document.getElementById('Id').value || '0'),
        FullName: document.getElementById('FullName').value,
        Email: document.getElementById('Email').value,
        Phone: document.getElementById('Phone').value,
        DateOfBirth: document.getElementById('DateOfBirth').value || null,
        Address: document.getElementById('Address').value
    };
}

function fillForm(c) {
    document.getElementById('Id').value = c.id || 0;
    document.getElementById('FullName').value = c.fullName || '';
    document.getElementById('Email').value = c.email || '';
    document.getElementById('Phone').value = c.phone || '';
    document.getElementById('DateOfBirth').value = c.dateOfBirth ? c.dateOfBirth.split('T')[0] : '';
    document.getElementById('Address').value = c.address || '';
}

function resetForm() {
    fillForm({id:0, fullName:'', email:'', phone:'', dateOfBirth:null, address:''});
    clearErrors();
}

async function loadTable(filter='') {
    const list = await fetchAll();
    const tbody = document.querySelector('#contactsTable tbody');
    tbody.innerHTML = '';
    const filtered = list.filter(c => {
        if (!filter) return true;
        const f = filter.toLowerCase();
        return (c.fullName||'').toLowerCase().includes(f) || (c.email||'').toLowerCase().includes(f);
    });
    filtered.forEach(c => {
        const tr = document.createElement('tr');
        tr.innerHTML = `<td>${c.fullName || ''}</td>
                        <td>${c.email || ''}</td>
                        <td>${c.phone || ''}</td>
                        <td>${c.dateOfBirth ? new Date(c.dateOfBirth).toLocaleDateString() : ''}</td>
                        <td>${c.address || ''}</td>
                        <td>
                            <button class='btn btn-sm btn-primary me-1' onclick='edit(${c.id})'>Edit</button>
                            <button class='btn btn-sm btn-danger' onclick='del(${c.id})'>Delete</button>
                        </td>`;
        tbody.appendChild(tr);
    });
}

window.edit = async function(id) {
    const res = await axios.get(apiBase + '/' + id);
    fillForm(res.data);
    // open modal
    const modalEl = document.getElementById('contactModal');
    const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
    modal.show();
}

window.del = async function(id) {
    if (!confirm('Delete this contact?')) return;
    await axios.delete(apiBase + '/' + id);
    await loadTable(document.getElementById('searchBox').value);
}

document.addEventListener('DOMContentLoaded', function(){
    loadTable();

    document.getElementById('contactForm').addEventListener('submit', async function(e){
        e.preventDefault();
        const contact = readForm();
        if (!validateClient(contact)) return;
        if (contact.Id && contact.Id > 0) {
            await axios.put(apiBase + '/' + contact.Id, contact);
        } else {
            await axios.post(apiBase, contact);
        }
        const modalEl = document.getElementById('contactModal');
        const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
        modal.hide();
        resetForm();
        await loadTable(document.getElementById('searchBox').value);
    });

    document.getElementById('searchBox').addEventListener('input', function(e){
        loadTable(e.target.value);
    });

    document.getElementById('btnAdd').addEventListener('click', function(){
        document.getElementById('contactModalLabel').innerText = 'Add Contact';
        resetForm();
    });
});

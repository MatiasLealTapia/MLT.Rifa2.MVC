function willDelete(id, controlador) {
    Swal.fire({
        title: '¿Desea eliminar?',
        text: 'Esta acción no se puede deshacer',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#dc3545',
        cancelButtonColor: '#6c757d',
        confirmButtonText: '<form action="/' + controlador + '/Delete" method="post"><input type="hidden" id="idObj" name="idObj" value="' + id + '" /><button class="p-0 m-0 swal2-confirm swal2-styled swal2-default-outline" aria-label="" style="display: inline-block; background-color: rgb(220, 53, 69);" type="submit">Eliminar</button></form>',
        cancelButtonText: 'Cancelar'
    })
}

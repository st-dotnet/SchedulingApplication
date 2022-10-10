// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$(document).ready(function () {
	$(function () {
		var current = location.pathname;
		$('#kt_aside_menu .menu-item a.menu-link').each(function () {
			var $this = $(this);
			// if the current path is like this link, make it active
			if ($this.attr('href').indexOf(current) !== -1) {
				$this.addClass('active');
			}
		})
	})
});
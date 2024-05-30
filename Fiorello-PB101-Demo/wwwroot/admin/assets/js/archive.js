"use strict"

let categoryArchiveBtns = document.querySelector(".add-archive")

categoryArchiveBtns.forEach(item =>
    item.addEventListener("click", function () {
        /* console.log(this)*/
        let id = parseInt(this.getAttribute("data-id"));

        (async () => {
            await fetch(`category/settoarchive?id=${id}`, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },

            });
            this.closest(".category-data").remove();
        })();
    })
        
)

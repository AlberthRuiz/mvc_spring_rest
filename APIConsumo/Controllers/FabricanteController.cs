using APIConsumo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace APIConsumo.Controllers {
    public class FabricanteController : Controller {
        private readonly string url = "http://localhost:8080";
        private readonly HttpClient _httpClient;

        public FabricanteController() {
            this._httpClient = new HttpClient();

        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            var result = await _httpClient.GetAsync($"{url}/api/v1/fabricantes");

            if (!result.IsSuccessStatusCode)                
                return BadRequest();
            
            var content = await result.Content.ReadAsStringAsync();
            IEnumerable<Fabricante> lst = JsonConvert.DeserializeObject<List<Fabricante>>(content)!;
            return View(lst);
        }




        public async Task<IActionResult> Edit(int id) {
            var result = await _httpClient.GetAsync($"{url}/api/v1/fabricante/{id}");            
            if (!result.IsSuccessStatusCode)                
                return BadRequest();
            
            var content = await result.Content.ReadAsStringAsync();
            
            var fabricante = JsonConvert.DeserializeObject<Fabricante>(content)!;
            return View(fabricante);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Fabricante fabricante) {
            if (ModelState.IsValid) {
                var json = JsonConvert.SerializeObject(fabricante);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{url}/api/v1/fabricante", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", "Error al actualizar el fabricante");
            }
            return View(fabricante);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Fabricante fabricate) {
            if (ModelState.IsValid) {
                var json = JsonConvert.SerializeObject(fabricate);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{url}/{id}", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "Error al actualizar el fabricante");
            }
            return View(fabricate);
        }
        public async Task<IActionResult> Delete(int id) {
            var result = await _httpClient.GetAsync($"{url}/{id}");
            if (!result.IsSuccessStatusCode)
                return NotFound();
            var content = await result.Content.ReadAsStringAsync();
            var fabricate = JsonConvert.DeserializeObject<Fabricante>(content)!;
            return View(fabricate);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var result = await _httpClient.DeleteAsync($"{url}");
            if (result.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Delete));
        }



    }
}

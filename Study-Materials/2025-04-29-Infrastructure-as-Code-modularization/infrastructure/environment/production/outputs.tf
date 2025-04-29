output "web_app_info" {
  description = "The IP addres to reach the server on"
  value = module.web_app.server_ip
}

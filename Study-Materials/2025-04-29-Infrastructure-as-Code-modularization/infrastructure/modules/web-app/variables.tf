variable "location" {
  description = "The location to place the server"
  type = string
}

variable "instance_type" {
  description = "The size of the instance"
  type = string
}

variable "ssh_public_key_id" {
  description = "SSH Public Key ID for accessing the server"
  type = string
}

variable "application_source" {
  description = "Application Source Address"
  type = string
}

variable "project_path" {
  description = "Project path"
  type = string
}

variable "project_name" {
  description = "Project name"
  type = string
}

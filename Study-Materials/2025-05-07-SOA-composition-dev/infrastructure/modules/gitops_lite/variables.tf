variable "ssh_public_key" {
  description = "Public keyes that the server trusts"
  type        = string
}

variable "server_type" {
  description = "The server type to use"
  type        = string
}

variable "location" {
  description = "The location of the server"
  type        = string
}

variable "git_source" {
  description = "Git repository to use as source repo"
  type        = string
}

variable "target_branch" {
  description = "The branch to target for reconciliation"
  type        = string
}

variable "compose_path" {
  description = "Path to the compose file inside the repo"
  type        = string
}

variable "update_interval" {
  description = "How often the server will sync with the repo"
  type        = string
}
